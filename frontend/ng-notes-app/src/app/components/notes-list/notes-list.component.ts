import { Component, numberAttribute } from '@angular/core';
import { NotesApiService } from '../../services/notes-api.service';
import { NoteWithCategories } from '../../../models/note-with-categories.model';
import { FirstFiveWordsPipe } from '../../pipes/first-five-words.pipe';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Note } from '../../../models/note.model';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Category } from '../../../models/category.model';

@Component({
    selector: 'app-notes-list',
    standalone: true,
    imports: [FirstFiveWordsPipe, RouterLink, RouterOutlet, ReactiveFormsModule],
    templateUrl: './notes-list.component.html',
    styleUrl: './notes-list.component.css',
})
export class NotesListComponent {
	notes: NoteWithCategories[] = [];
	auxNotes: NoteWithCategories[] = [];
	archivedNotes: NoteWithCategories[] = [];
	auxArchivedNotes: NoteWithCategories[] = [];
	archived: boolean = false;
	categories: Category[] = [];
	selectedCategoryId = new FormControl('0');
	selectedCategoryArchivedId = new FormControl('0');

	constructor(private notesApiService: NotesApiService) {}

	ngOnInit(): void {
		this.selectedCategoryId.valueChanges.subscribe(value => {
			this.filterNotes(value ?? '0');
		});
		this.selectedCategoryArchivedId.valueChanges.subscribe(value => {
			this.filterNotesArchived(value ?? '0');
		});
		this.notesApiService.getArchivedStatus().subscribe(status => {
			this.archived = status;
			this.getNotes();
			this.getArchivedNotes();
			this.getCategories();
		});
	}

	filterNotes(id: string) {
		if (id === '0') {
			this.notes = this.auxNotes;
			return;
		}
		const categoryId = Number(id); // Convert id to a number
		this.notes = this.auxNotes.filter(n => n.categories.some(c => c.id === categoryId));
	}

	filterNotesArchived(id: string) {
		if (id === '0') {
			this.archivedNotes = this.auxArchivedNotes;
			return;
		}
		const categoryId = Number(id); // Convert id to a number
		this.archivedNotes = this.auxArchivedNotes.filter(n => n.categories.some(c => c.id === categoryId));
	}

	getCategories(): void {
		this.notesApiService.getCategoriesAsync().subscribe((categories) => {
			this.categories = categories
		});
	}

	getNotes(): void {
		this.notesApiService
			.getNotesAsync(false)
			.subscribe((notes) => {
				this.notes = notes;
				this.auxNotes = notes;
			});
	}

	getArchivedNotes(): void {
		this.notesApiService
			.getNotesAsync(true)
			.subscribe((archivedNotes) => {
				this.archivedNotes = archivedNotes;
				this.auxArchivedNotes = archivedNotes;
			});
	}

	deleteNote(note: NoteWithCategories, isArchived: boolean){
		const noteToDelete : Note = {
			id: note.id,
			title: note.title,
			content: note.content,
			isArchived: note.isArchived,
			createdTime: note.createdTime,
			lastUpdatedTime: note.lastUpdatedTime,
		} 

		if (isArchived) {
			this.archivedNotes = this.archivedNotes.filter(n => n.id !== note.id);
		}
		else {
			this.notes = this.notes.filter(n => n.id !== note.id);
		}

		this.notesApiService.deleteNoteAsync(noteToDelete).subscribe();
	}

	archiveNote(note: NoteWithCategories, isArchived: boolean){
		const noteToArchive : Note = {
			id: note.id,
			title: note.title,
			content: note.content,
			isArchived: !note.isArchived,
			createdTime: note.createdTime,
			lastUpdatedTime: note.lastUpdatedTime,
		} 

		if (isArchived) {
			this.archivedNotes = this.archivedNotes.filter(n => n.id !== note.id);
			this.notes.push(note);
		}
		else {
			this.notes = this.notes.filter(n => n.id !== note.id);
			this.archivedNotes.push(note);
		}
		this.notesApiService.archiveNoteAsync(noteToArchive).subscribe();
	}
}
