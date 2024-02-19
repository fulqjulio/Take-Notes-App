import { Routes } from '@angular/router';
import { NotesListComponent } from './components/notes-list/notes-list.component';
import { NewNoteComponent } from './components/new-note/new-note.component';
import { EditNoteComponent } from './components/edit-note/edit-note.component';
import { CategoriesComponent } from './components/categories/categories.component';

export const routes: Routes = [
    { path: '', redirectTo: '/notes', pathMatch: 'full' },
    { path: 'notes', component: NotesListComponent },
    { path: 'newnote', component: NewNoteComponent },
    { path: 'editnote/:id', component: EditNoteComponent },
    { path: 'categories', component: CategoriesComponent },
    { path: '**', redirectTo: '/notes', pathMatch: 'full' },
];
