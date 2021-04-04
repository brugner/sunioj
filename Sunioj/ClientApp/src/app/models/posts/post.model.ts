import { Tag } from "./tag.model";

export class Post {
    id: number;
    title: string;
    summary: string;
    slug: string;
    content: string;
    tags: Tag[];
    createdAt: string;
    updatedAt: string;
    isDraft: boolean;
}
