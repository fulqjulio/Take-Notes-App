import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { NotesApiService } from '../../services/notes-api.service';
import { ActivatedRoute } from '@angular/router';
import { NoteWithCategories } from '../../../models/note-with-categories.model';
import { NoteAndCategory } from '../../../models/note-and-category.model';
import { Category } from '../../../models/category.model';

@Component({
    selector: 'app-edit-note',
    standalone: true,
    imports: [RouterOutlet, RouterModule, ReactiveFormsModule],
    templateUrl: './edit-note.component.html',
    styleUrl: './edit-note.component.css',
})
export class EditNoteComponent {
    editnote = new FormGroup({
        title: new FormControl('', Validators.required),
        content: new FormControl('', Validators.required),
    });
    note: NoteWithCategories = {} as NoteWithCategories;
	categoriesToAdd: Category[] = [];

    constructor(
        private notesApiService: NotesApiService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}

    async ngOnInit(): Promise<void> {
        await Promise.all([this.getNoteWithId(), this.getCategories()]);
		this.updateCategoriesToAdd();
    }

    getNoteWithId(): Promise<void> {
        const noteId: number = parseInt(
            this.route.snapshot.paramMap.get('id') ?? '0',
        );

        if (noteId === 0) {
            this.router.navigate(['/notes']);
        }

        return new Promise((resolve, reject) => {
            this.notesApiService.getNoteByIdAsync(noteId).subscribe({
                next: (note) => {
                    this.note = note;
                    this.editnote.controls.title.setValue(note.title);
                    this.editnote.controls.content.setValue(note.content);
                    resolve();
                },
                error: (error) => {
                    console.error('Error getting note:', error);
                    reject(error);
                }
            });
        });
    }

	getCategories(): Promise<void> {
        return new Promise((resolve, reject) => {
            this.notesApiService.getCategoriesAsync().subscribe({
                next: (categories) => {
                    this.categoriesToAdd = categories;
                    resolve();
                },
                error: (error) => {
                    console.error('Error getting categories:', error);
                    reject(error);
                }
            });
        });
    }

	updateCategoriesToAdd(){
		this.categoriesToAdd = this.categoriesToAdd.filter(category => !this.note.categories.some(noteCategory => noteCategory.id === category.id));
	}

    save() {
        const noteId: number = parseInt(
            this.route.snapshot.paramMap.get('id') ?? '0',
        );

        const note = {
            id: noteId,
            title: this.editnote.controls.title.value ?? '',
            content: this.editnote.controls.content.value ?? '',
            isArchived: this.note.isArchived,
            createdTime: this.note.createdTime,
            lastUpdatedTime: new Date(),
        };

        this.notesApiService.updateNoteAsync(note).subscribe(() => {
            this.router.navigate(['/notes']);
        });
    }

	addCategoryToNote(id: number){
		const noteAndCategory : NoteAndCategory = {
			id: 0,
			noteId: this.note.id,
			categoryId: id,
		}
        
        this.note.categories.push(this.categoriesToAdd.find(category => category.id === id) ?? {} as Category);
        this.updateCategoriesToAdd();


		this.notesApiService.addCategoryToNotesAsync(noteAndCategory).subscribe();
	}

    deleteCategoryFromNote(id: number){
        const noteAndCategory : NoteAndCategory = {
            id: 0,
            noteId: this.note.id,
            categoryId: id,
        }

        this.categoriesToAdd.push(this.note.categories.find(category => category.id === id) ?? {} as Category);
		this.note.categories = this.note.categories.filter(category => category.id !== id);

		this.notesApiService.deleteCategoryFromNotesAsync(noteAndCategory).subscribe();
    }
}
