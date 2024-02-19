import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NotesApiService } from './services/notes-api.service';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterLink, RouterOutlet],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
})
export class AppComponent {
    archived: boolean = false;

    constructor(public notesApiService: NotesApiService) {}

    ngOnInit(): void {
      this.notesApiService.getArchivedStatus().subscribe(status => {
        this.archived = status;
        });
    }
}
