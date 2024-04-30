import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {User} from '@/entities/user/User.ts';
import {CreateUser} from '@/entities/user/CreateUser.ts';

export class UserService {
    static async getAll() {
        return await axiosInstance.get<User[]>(apiMap.GET_USERS);
    }

    static async getAllRangers() {
        return await axiosInstance.get<User[]>(apiMap.GET_RANGERS);
    }

    static async create(user:CreateUser) {
        return await axiosInstance.post(apiMap.CREATE_USER, user);
    }

    static async update(user: User) {
        const url = `${apiMap.UPDATE_USER}/${user.id}`;
        return await axiosInstance.put(url, user);
    }

    static getFullName(user?:User) {
        return `${user?.lastName} ${user?.firstName} ${user?.middleName}`.trim();
    }
}