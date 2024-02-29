'use client';

import { ReactNode, useEffect } from 'react';

import { useActions } from '@/shared/lib/hooks/useActions';

type Props = {
    children: ReactNode
};

export const AuthProvider = ({ children }: Props) => {
    const { initAuth } = useActions();

    useEffect(() => {
        initAuth();
    }, [initAuth]);

    return children;
};