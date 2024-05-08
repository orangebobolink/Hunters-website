import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {HuntingLicense} from '@/entities/huntinLicense/HuntingLicense.ts';

export class HuntingLicenseService {
    static async getAll() {
        return await axiosInstance.get<HuntingLicense[]>(apiMap.GET_HUNTINGLICENSE);
    }

    static async getByUserId(id:string) {
        const url = `${apiMap.GET_HUNTINGLICENSE}/${id}`;
        return await axiosInstance.get<HuntingLicense>(url);
    }

    static async create(trip:HuntingLicense) {
        return await axiosInstance.post(apiMap.GET_HUNTINGLICENSE, trip);
    }

    static async update(huntingLicense: HuntingLicense) {
        const url = `${apiMap.GET_HUNTINGLICENSE}/${huntingLicense.id}`;
        return await axiosInstance.put(url, huntingLicense);
    }

    static async delete(id:string) {
        const url = `${apiMap.GET_HUNTINGLICENSE}/${id}`;
        return await axiosInstance.delete(url);
    }
}