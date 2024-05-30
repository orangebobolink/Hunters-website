const Page404 = () => {
    return (
        <div className="flex flex-col items-center justify-center min-h-screen p-4">
            <h1 className="text-6xl font-bold">404</h1>
            <p className="mt-4 text-xl">Go Home</p>
            <button className="mt-6 text-lg text-blue-600 hover:underline">
                <a href="/">Go Home</a>
            </button>
        </div>
    );
};

export default Page404;