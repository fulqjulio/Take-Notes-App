import { BaseEntity } from "./base-entity.model";

export interface Note extends BaseEntity {
    title: string;
    content: string;
    isArchived: boolean;
    createdTime: Date;
    lastUpdatedTime: Date;
}
