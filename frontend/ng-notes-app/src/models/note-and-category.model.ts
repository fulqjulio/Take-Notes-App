import { BaseEntity } from "./base-entity.model";

export interface NoteAndCategory extends BaseEntity {
    noteId: number;
    categoryId: number;
  }