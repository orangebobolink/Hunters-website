import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {RentProduct} from '@/entities/rent/models/RentProduct.ts';

export class RentProductService {
    static async getAll() {
        return await axiosInstance.get<RentProduct[]>(apiMap.GET_RENT_PRODUCT);
    }

    static async create(product:RentProduct) {
        return await axiosInstance.post(apiMap.GET_RENT_PRODUCT, product);
    }

    static async update(product: RentProduct) {
        const url = `${apiMap.GET_RENT_PRODUCT}/${product.id}`;
        return await axiosInstance.put(url, product);
    }

    static async delete(id:string) {
        const url = `${apiMap.GET_RENT_PRODUCT}/${id}`;
        return await axiosInstance.delete(url);
    }
}