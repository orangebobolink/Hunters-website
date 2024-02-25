import {BackendEndpoints} from '@/shared/const/BackendEndpoints.ts';


const BASE_URL = process.env.NEXT_PUBLIC_BACKEND || 'http://localhost:8000';
const BASE_API_URL = process.env.NEXT_PUBLIC_BACKEND_API || 'http://localhost:8000/api';

export const apiMap = {
    BASE_URL,
    BASE_API_URL,
    GET_USER: `${BASE_URL}/${BackendEndpoints.GET_USER}`,
    LOGIN: `${BASE_API_URL}/${BackendEndpoints.LOGIN}`,
    REFRESH: `${BASE_API_URL}/${BackendEndpoints.REFRESH}`,
    LOGOUT: `${BASE_API_URL}/${BackendEndpoints.LOGOUT}`,
};