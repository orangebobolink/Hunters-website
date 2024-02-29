import { ErrorResponse, ErrorResponseFromAxios } from '@/shared/model/store/queries/typing/responses/Error';

export class ErrorUtils {
    static isTypedError(error: any): error is ErrorResponse {
        return (
            error.data && error.data
            && error.data.error !== undefined && error.data.statusCode !== undefined
        );
    }

    static isTypedErrorFromAxios(error: any): error is ErrorResponseFromAxios {
        return error.response !== undefined && ErrorUtils.isTypedError(error.response);
    }
}