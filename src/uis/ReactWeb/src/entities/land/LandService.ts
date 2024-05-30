import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {Land} from '@/entities/land/Land.ts';

export class LandService {
    static async getAll() {
        return await axiosInstance.get<Land[]>(apiMap.GET_LANDS);
    }
}