import { toast } from 'react-toastify';

export const toastError = (msg: string) => toast(msg, {
    position: 'bottom-right',
    type: 'error',
});

export const toastSuccess = (msg: string) => toast(msg, {
    position: 'bottom-right',
    type: 'success',
});