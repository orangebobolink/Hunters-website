import {ThemeProvider} from '@/app/providers/theme-provider.tsx';
import {Router} from '@/app/router.tsx';
import {BrowserRouter} from 'react-router-dom';

function App() {
    return (
        <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
            <BrowserRouter>
                <Router/>
            </BrowserRouter>
        </ThemeProvider>
    )
}

export default App
