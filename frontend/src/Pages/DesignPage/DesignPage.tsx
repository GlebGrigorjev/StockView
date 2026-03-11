import RatioList from "../../Components/RatioList/RatioList";
import Table from "../../Components/Table/Table";

type Props = {}

const DesignPage = (props: Props) => {
  return  (
    <>
        <h1>StockView Design Page</h1>
        <h2>This is a design page. This is where we will house all the design aspects of the App</h2>
        <RatioList />
        <Table />
    </>
  );
}

export default DesignPage