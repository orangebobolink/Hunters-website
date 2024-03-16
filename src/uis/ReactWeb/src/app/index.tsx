import {ThemeProvider} from '@/app/providers/theme-provider.tsx';
import {Router} from '@/app/router.tsx';
import {BrowserRouter} from 'react-router-dom';
import {ReduxProvider} from '@/app/providers/redux-provider.tsx';
import {AuthProvider} from '@/app/providers/auth-provider.tsx';

function App() {
    return (
        <ReduxProvider>
            <AuthProvider>
                <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
                    <BrowserRouter>
                        <Router/>
                    </BrowserRouter>
                </ThemeProvider>
            </AuthProvider>
        </ReduxProvider>
    )
}

export default App
