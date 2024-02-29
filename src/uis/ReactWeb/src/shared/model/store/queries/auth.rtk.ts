import { BackendEndpoints } from '@/shared/const';
import { api } from '@/shared/model/store/queries/api';
import { RegisterRequest } from '@/shared/model/store/queries/typing/requests/RegisterRequest';
import { RegisterResponse } from '@/shared/model/store/queries/typing/responses/RegisterResponse';

const authRtk = api.injectEndpoints({
    endpoints: (build) => ({
        register: build.mutation<RegisterResponse, RegisterRequest>({
            query: (data: RegisterRequest) => ({
                body: data,
                url: BackendEndpoints.REGISTER,
                method: 'POST',
            }),
        }),
    }),
});

export const { useRegisterMutation } = authRtk;