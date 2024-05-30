import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {Permission} from '@/entities/permision/Permision.ts';

export class PermissionService {
    static async getAll() {
        return await axiosInstance.get<Permission[]>(apiMap.GET_PERMISSION+ "/include");
    }

    static async create(permission:Permission) {
        return await axiosInstance.post(apiMap.GET_PERMISSION, permission);
    }

    static async update(permission: Permission) {
        const url = `${apiMap.GET_PERMISSION}/${permission.id}`;
        return await axiosInstance.put(url, permission);
    }

    static async delete(id:string) {
        const url = `${apiMap.GET_PERMISSION}/${id}`;
        return await axiosInstance.delete(url);
    }
}