param()
$code = '
public class bgfilereader
{
	bool m_busy;
	public bool busy{get{return m_busy;}}
	string m_filename;
	public string filename{get{return m_filename;}}
	string[] m_resultlines;
	public string[] resultlines{get{return m_resultlines;}}
	protected object m_result;
	public object result{get{return m_result;}}
	System.ComponentModel.BackgroundWorker m_bgworker;
	public bgfilereader(string provided_filename)
	{
		m_resultlines = null;
		m_filename = provided_filename;
		m_bgworker = new System.ComponentModel.BackgroundWorker();
		m_bgworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(wcompleted);
		m_bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(wdowork);
		m_busy = true;
		m_bgworker.RunWorkerAsync(null);
	}
	private void wdowork(object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		System.IO.StreamReader reader = new System.IO.StreamReader(m_filename);
		string line = string.Empty;
		while((line = reader.ReadLine()) != null)
		{
			if(null == m_resultlines)
			{
				m_resultlines = new string[1];
				m_resultlines[0] = line;
			}
			else
			{
				string[] templ = new string[m_resultlines.Length + 1];
				for(int icnt = 0; icnt < m_resultlines.Length; icnt++)
				{
					templ[icnt] = m_resultlines[icnt];
				}
				templ[templ.Length - 1] = line;
				m_resultlines = templ;
			}
		}
		reader.Close();
		e.Result = m_resultlines;
	}
	private void wcompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
	{
		if(m_busy){}
		m_busy = false;
	}
}
'

$ral = @();
add-type -typedefinition $code -referencedassemblies $ral;
