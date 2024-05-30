import { createAsyncThunk } from '@reduxjs/toolkit';

type IsPaidThunkArg = {
    flag: boolean;
};

type IsPaidThunkPayload = boolean;

export const isPaidThunk = createAsyncThunk<IsPaidThunkPayload, IsPaidThunkArg>(
    'auth/isPaid',
    async ({ flag }, thunkAPI) => {
        try {
            return flag;
        } catch (error) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);
