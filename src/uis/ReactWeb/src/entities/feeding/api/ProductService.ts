import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';

export class ProductService {
    static async getAll() {
        return await axiosInstance.get<string[]>(apiMap.GET_PRODUCTS);
    }
}