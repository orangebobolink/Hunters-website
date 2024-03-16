import axios from 'axios';

import { LoginRequest } from '@/shared/model/store/queries/typing/requests/LoginRequest';
import { LoginResponse } from '@/shared/model/store/queries/typing/responses/LoginResponse';
import {apiMap} from '@/shared/const';
import {LocaleStorageUtils} from '@/shared/lib';

export class AuthService {
    static async login(data: LoginRequest) {
        return axios.post<LoginResponse>(apiMap.LOGIN, data, {
            withCredentials: true,
        });
    }

    static async refresh() {
        const accessToken = `Bearer ${LocaleStorageUtils.getAccessToken()}`

        return axios.post(apiMap.REFRESH, {}, {
            withCredentials: true,
            headers:{
                Authorization:accessToken
             }
        });
    }

    static async logout() {
        return axios.post(apiMap.LOGOUT, {}, { withCredentials: true });
    }
}