import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { NoteWithCategories } from '../../models/note-with-categories.model';
import { Note } from '../../models/note.model';
import { Category } from '../../models/category.model';
import { NoteAndCategory } from '../../models/note-and-category.model';

@Injectable({
  providedIn: 'root'
})
export class NotesApiService {
  private archived = new BehaviorSubject<boolean>(false);
  private apiUrl = environment.apiUrl;
  private headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Ocp-Apim-Subscription-Key': environment.apiSuibscriptionKey});

  constructor(private http: HttpClient) { }


  // Archived status

  getArchivedStatus(): Observable<boolean> {
    return this.archived.asObservable();
  }

  changeArchived(): void {
    this.archived.next(!this.archived.value);
  }

  // Notes Actions

  getNoteByIdAsync(id: number): Observable<NoteWithCategories> {
    return this.http.get<NoteWithCategories>(`${this.apiUrl}/NoteAndCategory/GetNoteWithCategoryById?Id=${id}`, { headers: this.headers });
  }

  getNotesAsync(archived: boolean): Observable<NoteWithCategories[]> {
    return this.http.get<NoteWithCategories[]>(`${this.apiUrl}/NoteAndCategory/GetNoteWithCategoryArchived?archived=${archived}`, { headers: this.headers });
  }

  addNoteAsync(note: Note): Observable<Note> {
    return this.http.post<Note>(`${this.apiUrl}/Note/CreateNoteAsync`, note, { headers: this.headers });
  }

  updateNoteAsync(note: Note): Observable<any> {
    return this.http.patch(`${this.apiUrl}/Note/UpdateNoteAsync`, note, { headers: this.headers });
  }

  deleteNoteAsync(note: Note): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Note/DeleteNoteAsync`, { body: note, headers: this.headers });
  }

  archiveNoteAsync(note: Note): Observable<any> {
    return this.http.patch(`${this.apiUrl}/Note/UpdateNoteAsync`, note, { headers: this.headers });
  }

  // Categories Actions

  getCategoriesAsync(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.apiUrl}/Category/GetAllCategoriesAsync`, { headers: this.headers });
  }
  
  createCategoryAsync(category: Category): Observable<any> {
    return this.http.post(`${this.apiUrl}/Category/CreateCategoryAsync`, category, { headers: this.headers });
  }

  deleteCategoryAsync(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Category/DeleteCategoryByIdAsync?Id=${id}`, { headers: this.headers });
  }


  // Note and Category Actions

  addCategoryToNotesAsync(noteAndCategory: NoteAndCategory): Observable<any> {
    return this.http.post(`${this.apiUrl}/NoteAndCategory/AddCategoryToNote`, noteAndCategory, { headers: this.headers });
  }

  deleteCategoryFromNotesAsync(noteAndCategory: NoteAndCategory): Observable<any> {
    return this.http.delete(`${this.apiUrl}/NoteAndCategory/DeleteCategoryFromNote`, { body: noteAndCategory, headers: this.headers });
  }
}
