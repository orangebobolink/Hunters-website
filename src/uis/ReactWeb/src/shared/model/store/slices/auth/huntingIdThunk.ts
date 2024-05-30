import { createAsyncThunk } from '@reduxjs/toolkit';

type HuntingIdThunkArg = {
    id: string;
};

type HuntingIdThunkPayload = string;

export const huntingIdThunk = createAsyncThunk<
    HuntingIdThunkPayload,
    HuntingIdThunkArg
>('auth/huntingId', async ({ id }, thunkAPI) => {
    try {
        return id;
    } catch (error) {
        return thunkAPI.rejectWithValue(error.message);
    }
});
