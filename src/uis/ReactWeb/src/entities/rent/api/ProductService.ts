import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {Product} from '@/entities/rent/models/Product.ts';

export class ProductService {
    static async getAll() {
        return await axiosInstance.get<Product[]>(apiMap.GET_PRODUCT);
    }

    static async create(product:Product) {
        return await axiosInstance.post(apiMap.GET_PRODUCT, product);
    }

    static async update(product: Product) {
        const url = `${apiMap.GET_PRODUCT}/${product.id}`;
        return await axiosInstance.put(url, product);
    }

    static async delete(id:string) {
        const url = `${apiMap.GET_PRODUCT}/${id}`;
        return await axiosInstance.delete(url);
    }
}