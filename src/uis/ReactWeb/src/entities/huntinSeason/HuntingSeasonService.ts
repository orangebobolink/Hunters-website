import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {HuntingSeason} from '@/entities/huntinSeason/HuntingSeason.ts';

export class HuntingService {
    static async create(huntingSeason:HuntingSeason) {
        return await axiosInstance.post(apiMap.GET_HUNTINGSEASSON, huntingSeason);
    }

    static async remove(id:string) {
        const url = `${apiMap.GET_HUNTINGSEASSON}/${id}`;
        return await axiosInstance.delete(url);
    }

    static async update(huntingSeason: HuntingSeason) {
        const url = `${apiMap.GET_HUNTINGSEASSON}/${huntingSeason.id}`;
        return await axiosInstance.put(url, huntingSeason);
    }
}