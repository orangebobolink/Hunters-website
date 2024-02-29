export interface ErrorResponse {
    data: {
        message: string,
        error: string,
        statusCode: number
    }
}

export interface ErrorResponseFromAxios {
    response: {
        data: {
            message: string,
            error: string,
            statusCode: number
        }
    }
}