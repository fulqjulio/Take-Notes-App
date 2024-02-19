import { BaseEntity } from "./base-entity.model";
import { Category } from "./category.model";

export interface NoteWithCategories extends BaseEntity {
    title: string;
    content: string;
    isArchived: boolean;
    createdTime: Date;
    lastUpdatedTime: Date;
    categories: Category[];
  }