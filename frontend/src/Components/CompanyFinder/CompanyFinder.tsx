import { useEffect, useState } from "react";
import { CompanyCompData } from "../../company";
/* import { getPeerComparisonData } from "../../api"; */
import CompanyFinderItem from "./CompanyFinderItem/CompanyFinderITem";

type Props = {
  ticker: string;
}

const CompanyFinder = ({ticker}: Props) => {
    const [companyData, setCompanyData] = useState<CompanyCompData[]>([]);
    useEffect(() => {
        const getComps = async () => {
/*             const value = await getPeerComparisonData(ticker); */
/*             setCompanyData(value?.data || []); */
        };
        getComps();
    }, [ticker]);

  return (
  <div className="inline-flex rounded-md shadow-sm m-4">
    {/* {companyData.map((company) => {
      return <CompanyFinderItem ticker={company.symbol} />;
    })} */}
  </div>
);
}

export default CompanyFinder