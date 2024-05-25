import { useMemo } from 'react';
import { bindActionCreators } from 'redux';

import { useAppDispatch } from '@/shared/lib/hooks/redux-hooks';
import { authActions } from '@/shared/model/store/slices/auth';
import { loginThunk } from '@/shared/model/store/slices/auth/loginThunk';
import { logoutThunk } from '@/shared/model/store/slices/auth/logoutThunk';
import { refreshAuthThunk } from '@/shared/model/store/slices/auth/refreshAuthThunk';
import { isPaidThunk } from '@/shared/model/store/slices/auth/isPaidThunk';

const actions = {
    login: loginThunk,
    initAuth: refreshAuthThunk,
    logout: logoutThunk,
    changeIsPaid: isPaidThunk,
    ...authActions,
};

export const useActions = () => {
    const dispatch = useAppDispatch();
    return useMemo(() => bindActionCreators(actions, dispatch), [dispatch]);
};
