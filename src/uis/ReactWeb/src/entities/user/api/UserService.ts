import { apiMap } from '@/shared/const';
import { axiosInstance } from '@/shared/api/axiosInstance.ts';
import { User } from '@/entities/user/models/User.ts';
import { CreateUser } from '@/entities/user/models/CreateUser.ts';

export class UserService {
    static async getAll() {
        return await axiosInstance.get<User[]>(apiMap.GET_USERS);
    }

    static async getAllRangers() {
        return await axiosInstance.get<User[]>(apiMap.GET_RANGERS);
    }

    static async create(user: CreateUser) {
        return await axiosInstance.post(apiMap.CREATE_USER, user);
    }

    static async update(user: User) {
        const url = `${apiMap.UPDATE_USER}/${user.id}`;
        return await axiosInstance.put(url, user);
    }

    static async updatePassword(id: string, password: string) {
        const url = `${apiMap.UPDATE_USER}/password/${id}`;
        return await axiosInstance.put(url, { id: id, password: password });
    }

    static getFullName(user?: User) {
        return `${user?.lastName} ${user?.firstName} ${user?.middleName}`.trim();
    }

    static getLastAndFirstName(user?: User) {
        return `${user?.lastName} ${user?.firstName}`.trim();
    }

    static async getById(id: string) {
        const url = `${apiMap.GET_USERS}/${id}`;
        return await axiosInstance.get(url);
    }
}
