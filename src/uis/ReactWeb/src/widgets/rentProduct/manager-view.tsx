import {useEffect, useState} from 'react';
import RentProductTable from '@/features/table/RentProductTable.tsx';
import {RentProduct} from '@/entities/rent/models/RentProduct.ts';
import {RentProductService} from '@/entities/rent/api/RentProductService.ts';

const ManagerView = () => {
    const [rentProducts, setRentProducts] = useState<RentProduct[]>([])

    useEffect(() => {
        const fetchRentProducts = async () => {
            try {
                const response = await RentProductService.getAll();

                setRentProducts(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchRentProducts();
    }, []);


    return (
        <div className="select-none h-full w-full flex items-center flex-col justify-center space-y-5">
            <div className="w-2/3 flex justify-center">
            <RentProductTable rentProducts={rentProducts} setRentProducts={setRentProducts}/>
            </div>
        </div>
    );
};

export default ManagerView;