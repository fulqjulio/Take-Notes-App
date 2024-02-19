import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Note } from '../../../models/note.model';
import { NotesApiService } from '../../services/notes-api.service';
import { Router } from '@angular/router';


@Component({
    selector: 'app-new-note',
    standalone: true,
    imports: [RouterModule, ReactiveFormsModule],
    templateUrl: './new-note.component.html',
    styleUrl: './new-note.component.css',
})
export class NewNoteComponent {
    newnote = new FormGroup({
      title: new FormControl('', Validators.required),
      content: new FormControl('', Validators.required)
    });

    constructor(
        private notesApiService: NotesApiService,
        private router: Router,
    ) {}

    save() {
        const note: Note = {
            id: 0,
            title: this.newnote.controls.title.value ?? '',
            content: this.newnote.controls.content.value ?? '',
            isArchived: false,
            createdTime: new Date(),
            lastUpdatedTime: new Date(),
        };

        this.notesApiService.addNoteAsync(note).subscribe(() => {
            this.router.navigate(['/notes']);
        });
    }
}
