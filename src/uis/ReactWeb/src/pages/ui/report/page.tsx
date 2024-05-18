import ProductPopularBar from '@/entities/report/ui/product-popular-bar.tsx';
import ProductRevenueBar from '@/entities/report/ui/product-revenue-bar.tsx';

const ReportPage = () => {
    return (
        <div className="w-full flex flex-row flex-wrap justify-center items-center mt-10">
            <ProductPopularBar/>
            <ProductRevenueBar/>
        </div>
    );
};

export default ReportPage;