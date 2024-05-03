import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {Raid} from '@/entities/raid/Raid.ts';

export class RaidService {
    static async getAll() {
        return await axiosInstance.get<Raid[]>(apiMap.GET_RAIDS+ "/include");
    }

    static async getRaidsById(id:string) {
        const url = `${apiMap.GET_RAIDS}/${id}`;
        return await axiosInstance.get<Raid[]>(url);
    }

    static async create(raid:Raid) {
        return await axiosInstance.post(apiMap.GET_RAIDS, raid);
    }
}