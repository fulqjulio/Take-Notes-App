import { Component } from '@angular/core';
import { RouterLink, RouterModule, RouterOutlet } from '@angular/router';
import { Category } from '../../../models/category.model';
import { NotesApiService } from '../../services/notes-api.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
    selector: 'app-categories',
    standalone: true,
    imports: [RouterOutlet, RouterLink, RouterModule, ReactiveFormsModule],
    templateUrl: './categories.component.html',
    styleUrl: './categories.component.css',
})
export class CategoriesComponent {
    categories: Category[] = [];
	category = new FormGroup({
		name: new FormControl('', Validators.required),
	});

    constructor(private notesApiService: NotesApiService) {}

    ngOnInit(): void {
        this.getCategories();
    }

	getCategories(): void {
		this.notesApiService.getCategoriesAsync().subscribe((categories) => {
			this.categories = categories;
		});
	}

	createCategory(){
		const category = {
			id: 0,
			name: this.category.controls.name.value ?? '',
		};

		this.categories.push(category);

		this.notesApiService.createCategoryAsync(category).subscribe();

		this.category.controls.name.setValue('');
	}

	deleteCategory(id: number){	
		this.categories = this.categories.filter(c => c.id !== id);
		this.notesApiService.deleteCategoryAsync(id).subscribe();
	}
}
