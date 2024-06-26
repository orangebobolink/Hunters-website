import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { LocaleStorageUtils } from '@/shared/lib/utils';
import { loginThunk } from '@/shared/model/store/slices/auth/loginThunk';
import { logoutThunk } from '@/shared/model/store/slices/auth/logoutThunk';
import { refreshAuthThunk } from '@/shared/model/store/slices/auth/refreshAuthThunk';
import { LoginResponse } from '@/shared/model/store/queries/typing/responses/LoginResponse';
import { isPaidThunk } from './isPaidThunk';
import { huntingIdThunk } from './huntingIdThunk';

type InitialState = {
    id: string | null;
    isAuth: boolean;
    avatarUrl: string;
    username: string | null;
    roles: string[];
    huntingLicenseId: string;
    isPaid: boolean;
    isLoading: boolean;
    isSuccess: boolean;
    error: string | null;
};

const initialState: InitialState = {
    id: null,
    isAuth: false,
    username: null,
    roles: [],
    avatarUrl: '',

    huntingLicenseId: '',
    isPaid: false,
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

const setRejectedValues = (
    state: InitialState,
    action: PayloadAction<unknown>
) => {
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
            .addCase(
                loginThunk.fulfilled,
                (state, { payload }: PayloadAction<LoginResponse>) => {
                    state.id = payload.id;
                    state.username = payload.username;
                    state.roles = payload.roles;
                    state.isAuth = true;
                    state.isPaid = payload.isPaid;
                    state.avatarUrl = payload.avatarUrl;
                    state.huntingLicenseId = payload.huntingLicenseId;
                    LocaleStorageUtils.setAccessToken(payload.accessToken);

                    setFulfilledValues(state);
                    console.log(payload);
                }
            )
            .addCase(loginThunk.rejected, setRejectedValues)
            .addCase(refreshAuthThunk.pending, setPendingStatuses)
            .addCase(
                refreshAuthThunk.fulfilled,
                (state, { payload }: PayloadAction<LoginResponse>) => {
                    console.log(payload);
                    state.id = payload.id;
                    state.username = payload.username;
                    state.roles = payload.roles;
                    state.isAuth = true;
                    state.avatarUrl = payload.avatarUrl;
                    state.huntingLicenseId = payload.huntingLicenseId;
                    state.isPaid = payload.isPaid;
                    LocaleStorageUtils.setAccessToken(payload.accessToken);

                    setFulfilledValues(state);
                }
            )
            .addCase(refreshAuthThunk.rejected, setRejectedValues)
            .addCase(logoutThunk.pending, (state) => {
                state.id = null;
                state.isAuth = false;
                state.username = null;
                state.roles = [];
            })
            .addCase(logoutThunk.fulfilled, (state) => {
                setFulfilledValues(state);
                LocaleStorageUtils.removeAccessToken();
            })
            .addCase(logoutThunk.rejected, setRejectedValues)
            .addCase(isPaidThunk.pending, setPendingStatuses)
            .addCase(isPaidThunk.fulfilled, (state, action) => {
                state.isPaid = action.payload;
                setFulfilledValues(state);
            })
            .addCase(huntingIdThunk.pending, setPendingStatuses)
            .addCase(huntingIdThunk.fulfilled, (state, action) => {
                state.huntingLicenseId = action.payload;
                setFulfilledValues(state);
            });
    },
});

export default authSlice.reducer;
export const { ...authActions } = authSlice.actions;
