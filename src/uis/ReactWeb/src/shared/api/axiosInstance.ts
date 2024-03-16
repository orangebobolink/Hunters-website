import axios from 'axios';

import { LocaleStorageUtils } from '@/shared/lib/utils';
import {apiMap} from '@/shared/const';

export const axiosInstance = axios.create({
    baseURL: apiMap.BASE_API_URL,
    withCredentials: true,
});

axiosInstance.interceptors.request.use((config) => {
    config.headers.Authorization = `Bearer ${LocaleStorageUtils.getAccessToken()}`;
    return config;
});

axiosInstance.interceptors.response.use((config) => config, async (error) => {
    const originalRequest = error.config;
    if (error.response.status === 401 && error.config && !error.config.isRetry) {
        originalRequest.isRetry = true;
        const { data } = await axios.post<{ access: string }>(apiMap.REFRESH, {}, { withCredentials: true });
        LocaleStorageUtils.setAccessToken(data.access);
        return axiosInstance.request(originalRequest);
    }
    throw error;
});