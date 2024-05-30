import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {Feeding} from '@/entities/feeding/models/Feeding.ts';

export class FeedingService {
    static async getAll() {
        return await axiosInstance.get<Feeding[]>(apiMap.GET_FEEDING+ "/include");
    }

    static async create(feeding:Feeding) {
        return await axiosInstance.post(apiMap.GET_FEEDING, feeding);
    }

    static async update(feeding: Feeding) {
        const url = `${apiMap.GET_FEEDING}/${feeding.id}`;
        return await axiosInstance.put(url, feeding);
    }

    static async delete(id:string) {
        const url = `${apiMap.GET_FEEDING}/${id}`;
        return await axiosInstance.delete(url);
    }
}