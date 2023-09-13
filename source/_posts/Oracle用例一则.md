#### Oracle用例一则

```sql
select t.outpatient_id,
		t2.file_no,
		t3.patient_name,
		to_char(t.datetime_in,'yyyy-mm-dd') as datetime_in,
		t4.dept_name,
		t2.topic,
		nvl(t2.print_flag,0) as print_flag,
		t2.emr_doc
	from table1 t,
		table2 t2,
		table3 t3,
		table4 t4,
		table5 t5,
	where t.outpatient_id =t2.outpatient_id
		and t.patient_id=t3.patient_id
		and t2.dept_id=t4.dept_id
		and t.patient_id=t5.brid
		and t5.jzkh='2999999'
		and to_char(t2.CREATE_DATE_TIME,'yyyy-mm-dd')>=to_char(to_date('2018-06-01','yyyy-mm-dd')-7),'yyyy-mm-dd')
		and to_char(t2.CREATE_DATE_TIME,'yyyy-mm-dd')>'2011-05-01'
		order by t2.CREATE_DATE_TIME desc
		
   select to_char(sysdate-7,'yyyy-mm-dd') from dual;--查询距离今天7天的时间--将日期格式转为字符格式
   
   select * from t2 
   	where t2.outpatient_id='98839383738'
   	for update;--查询t2表中的数据并可修改
   	
   select to_date('2018-06-01','yyyy-mm-dd')-7 from dual;--将字符格式转为日期格式
```

