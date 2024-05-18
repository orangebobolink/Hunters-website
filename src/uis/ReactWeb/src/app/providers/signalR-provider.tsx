import {ReactNode, useState} from 'react';
import {LocaleStorageUtils} from '@/shared/lib';
import {SignalRContext} from '@/shared/model/signalrR';
import {LogLevel} from '@microsoft/signalr';

const SignalRProvider = ({ children }: { children: ReactNode })  => {
    const [token] = useState<string>(() => LocaleStorageUtils.getAccessToken()!);

    return (
        <SignalRContext.Provider
            connectEnabled={!!token}
            accessTokenFactory={() => token}
            dependencies={[token]}
            url={"http://localhost:5019/chat"}
            automaticReconnect={false}
            logger={LogLevel.Information}
        >
            {children}
        </SignalRContext.Provider>
    );
};

export default SignalRProvider;