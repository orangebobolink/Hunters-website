import { createAsyncThunk } from '@reduxjs/toolkit';

import { AuthService } from '@/shared/api/services/AuthService';
import { Notice } from '@/shared/const';
import { ErrorUtils } from '@/shared/lib/utils/ErrorUtils';
import type { LoginRequest } from '@/shared/model/store/queries/typing/requests/LoginRequest';
import { toast } from '@/shared/ui/use-toast';

export const loginThunk = createAsyncThunk(
    'identity/authorization/login',
    async ({ ...data }: LoginRequest & { controller: AbortController }, { rejectWithValue }) => {
        try {
            const response = await AuthService.login(data);

            return response.data;
        } catch (error: any) {
            toast({
                variant: "destructive",
                title: error.response.data.error,
            })
            if (ErrorUtils.isTypedErrorFromAxios(error)) {
                 
                return rejectWithValue(error.response.data.message);
            }
            return rejectWithValue(Notice.UNEXPECTED_ERROR);
        }
    },
);