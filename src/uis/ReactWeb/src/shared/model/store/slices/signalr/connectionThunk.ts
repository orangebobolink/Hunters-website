import {createAsyncThunk} from '@reduxjs/toolkit';
import * as signalR from "@microsoft/signalr";

export const connectToSignalR = createAsyncThunk(
    'signalr/connect',
    async (_, { getState }) => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("your_signalr_endpoint_here")
            .build();

        connection.on("receiveMessage", (message) => {
            // Действия для обработки полученного сообщения
        });

        try {
            await connection.start();
        } catch (err) {
            console.error(err);
        }

        // Возвращаем соединение, чтобы его можно было использовать в других местах приложения
        return connection;
    }
);