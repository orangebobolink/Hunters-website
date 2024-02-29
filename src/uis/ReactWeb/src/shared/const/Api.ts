import {BackendEndpoints} from '@/shared/const/BackendEndpoints.ts';


const BASE_URL = 'http://localhost:5001';
const BASE_API_URL = 'http://localhost:5001/api';

export const apiMap = {
    BASE_URL,
    BASE_API_URL,
    GET_USER: `${BASE_URL}/${BackendEndpoints.GET_USER}`,
    LOGIN: `${BASE_API_URL}/${BackendEndpoints.LOGIN}`,
    REFRESH: `${BASE_API_URL}/${BackendEndpoints.REFRESH}`,
    LOGOUT: `${BASE_API_URL}/${BackendEndpoints.LOGOUT}`,
};