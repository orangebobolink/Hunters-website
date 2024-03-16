import React from 'react';
import {LoginForm} from '@/widgets/forms/ui/LoginForm';

export const SignInPage: React.FC = () => {
    return (
        <div className="h-screen flex items-center justify-center flex-col">
            <LoginForm/>
        </div>
    )
}