import axios from 'axios';

import { LoginRequest } from '@/shared/model/store/queries/typing/requests/LoginRequest';
import { LoginResponse } from '@/shared/model/store/queries/typing/responses/LoginResponse';
import {apiMap} from '@/shared/const';

export class AuthService {
    static async login(data: LoginRequest, controller?: AbortController) {
        return axios.post<LoginResponse>(apiMap.LOGIN, data, {
            signal: controller?.signal,
            withCredentials: true,
        });
    }

    static async refresh() {
        return axios.post(apiMap.REFRESH, {}, { withCredentials: true });
    }

    static async logout() {
        return axios.post(apiMap.LOGOUT, {}, { withCredentials: true });
    }
}