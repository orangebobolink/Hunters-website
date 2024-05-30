import {Type} from '@/entities/rent/models/Type.ts';
import {RentProduct} from '@/entities/rent/models/RentProduct.ts';


export interface Product {
    id?: string;
    name: string;
    price: number;
    quantityInStock: number;
    description: string;
    imageUrl: string;
    createdAt?: Date;
    updatedAt?: Date;
    type:Type;
    rentProducts?: RentProduct[];
}