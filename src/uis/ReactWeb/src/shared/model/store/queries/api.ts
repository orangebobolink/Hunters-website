import type { BaseQueryApi, BaseQueryFn } from '@reduxjs/toolkit/dist/query/baseQueryTypes';
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import {LocaleStorageUtils} from '@/shared/lib/utils';
import {apiMap} from '@/shared/const';


const customBaseQuery: BaseQueryFn = async (args, api: BaseQueryApi, extraOptions) => {
    const query = fetchBaseQuery({
        baseUrl: apiMap.BASE_API_URL,
        credentials: 'include',
        prepareHeaders: ((headers) => {
            headers.set('Authorization', `Bearer ${LocaleStorageUtils.getAccessToken()}`);
            return headers;
        }),
    });

    let result = await query(args, api, extraOptions);

    if (result.error?.status === 401) {
        await api.dispatch(refreshAuthThunk);
        result = await query(args, api, extraOptions);
    }

    return result;
};

export const api = createApi({
    reducerPath: 'api',
    baseQuery: customBaseQuery,
    endpoints: () => ({}),
});