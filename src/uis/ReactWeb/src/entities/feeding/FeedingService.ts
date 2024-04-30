import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {Feeding} from '@/entities/feeding/Feeding.ts';

export class FeedingService {
    static async getAll() {
        return await axiosInstance.get<Feeding[]>(apiMap.GET_FEEDING);
    }

    static async create(feeding:Feeding) {
        return await axiosInstance.post(apiMap.GET_ANIMALS, feeding);
    }

    static async update(feeding: Feeding) {
        const url = `${apiMap.GET_ANIMALS}/${feeding.id}`;
        return await axiosInstance.put(url, feeding);
    }
}