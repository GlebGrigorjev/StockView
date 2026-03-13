import { CompanyKeyMetrics } from "../../company";
import RatioList from "../../Components/RatioList/RatioList";
import Table from "../../Components/Table/Table";

type Props = {}
const tableConfig = [
  {
    label: "Market Cap",
    render: (company: CompanyKeyMetrics) => company.marketCap,
    subTitle: "Total value of all a company's shares of stock",
  },
]

const DesignPage = (props: Props) => {
  return  (
    <>
        <h1>StockView Design Page</h1>
        <h2>This is a design page. This is where we will house all the design aspects of the App</h2>
        <RatioList config={tableConfig} data={{ marketCap: 1000000 } as CompanyKeyMetrics} />
        <Table config={tableConfig} data={[{ marketCap: 1000000 } as CompanyKeyMetrics]} />
    </>
  );
}

export default DesignPage