URL to call get and update:
1. Get: customersetting/enablestatus/productstatus/index?custid={0}&uplineid={1}&userid={2}&productid={3}&level={4}
	{0}: customer id (customer is selected to edit)
	{1}: upline id (parent customer id)
	{2}: user id (parent customer id or admin id)
	{3}: product id (string data type in Products.config of attribute [id] in element product, eg. <product id="casino" />)
	{4}: current level of customer (1: member, 2: agent, 3: master, 4: super)
2. Update: customersetting/enablestatus/productstatus/update?model={0}
	{0}: model that is generated into client