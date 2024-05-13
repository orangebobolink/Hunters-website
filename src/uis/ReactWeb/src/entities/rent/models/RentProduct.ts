import {User} from '@/entities/user/models/User.ts';
import {Product} from '@/entities/rent/models/Product.ts';

export interface RentProduct {
    id?:string;
    userId:string;
    user?:User;
    productId:string;
    product?:Product;
    fromDate?:Date;
    toDate?:Date;
    status?:string;
}