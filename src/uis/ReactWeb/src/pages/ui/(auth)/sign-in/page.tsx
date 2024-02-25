import React from 'react';
import {SignInForm} from '@/features/ui';

export const SignInPage: React.FC = () => {
    return (
        <div className="h-screen flex items-center justify-center flex-col">
            <SignInForm/>
        </div>
    )
}