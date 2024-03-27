import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { LocaleStorageUtils } from '@/shared/lib/utils';
import { loginThunk } from '@/shared/model/store/slices/auth/loginThunk';
import { logoutThunk } from '@/shared/model/store/slices/auth/logoutThunk';
import { refreshAuthThunk } from '@/shared/model/store/slices/auth/refreshAuthThunk';
import { LoginResponse } from '@/shared/model/store/queries/typing/responses/LoginResponse';

type InitialState = {
    id: string | null,
    isAuth: boolean,
    username: string | null,
    roles: string[],

    isLoading: boolean,
    isSuccess: boolean,
    error: string | null
};

const initialState: InitialState = {
    id: null,
    isAuth: false,
    username: null,
    roles: [],

    isLoading: false,
    isSuccess: false,
    error: null,
};

const setPendingStatuses = (state: InitialState) => {
    state.isLoading = true;
    state.isSuccess = false;
    state.error = null;
};

const setFulfilledValues = (state: InitialState) => {
    state.isSuccess = true;
    state.isLoading = false;
};

const setRejectedValues = (state: InitialState, action: PayloadAction<unknown>) => {
    state.error = action.payload as string;
    state.isSuccess = false;
    state.isLoading = false;
};

const authSlice = createSlice({
    initialState,
    name: 'auth',
    reducers: {
        resetStatuses(state) {
            state.isLoading = false;
            state.isSuccess = false;
            state.error = null;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(loginThunk.pending, setPendingStatuses)
            .addCase(loginThunk.fulfilled, (state, { payload }: PayloadAction<LoginResponse>) => {
                state.id = payload.id;
                state.username = payload.username
                state.roles = payload.roles;
                state.isAuth = true;
                LocaleStorageUtils.setAccessToken(payload.accessToken);

                setFulfilledValues(state);
            })
            .addCase(loginThunk.rejected, setRejectedValues)
            .addCase(refreshAuthThunk.pending, setPendingStatuses)
            .addCase(refreshAuthThunk.fulfilled, (state, { payload }: PayloadAction<LoginResponse>) => {
                state.id = payload.id;
                state.username = payload.username
                state.roles = payload.roles;
                state.roles = payload.roles;
                state.isAuth = true;
                LocaleStorageUtils.setAccessToken(payload.accessToken);

                setFulfilledValues(state);
            })
            .addCase(refreshAuthThunk.rejected, setRejectedValues)
            .addCase(logoutThunk.pending, (state) => {
                state.id = null;
                state.isAuth = false;
                state.username = null;
                state.roles = [];
                LocaleStorageUtils.removeAccessToken();
            })
            .addCase(logoutThunk.fulfilled, (state) => {
                setFulfilledValues(state);
            })
            .addCase(logoutThunk.rejected, setRejectedValues);
    },
});

export default authSlice.reducer;
export const { ...authActions } = authSlice.actions;