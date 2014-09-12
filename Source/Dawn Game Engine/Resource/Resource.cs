using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource
{
	public class Resource : EngineObject, IDisposable,ICloneable
	{
		public override string ObjectClassName() { return Define.EngineClassName.Resource(); }
		protected string _filename;
		protected bool _isLoad;
		public Resource()
		{
			_isLoad = false;
			_filename = "";
		}
		public Resource(string filename)
			: this()
		{
			_filename = filename;
		}
		~Resource()
		{
			Dispose();
		}
		public void Dispose()
		{
			if (isLoad())
			{
				Unload();
			}
		}
		public virtual void Load()
		{
			if (isLoad())
			{
				DGE.Debug.Error(this, Define.EngineErrorName.Resource_CannotLoad(), GetErrorDetail());
			}
			_isLoad = true;
		}

		public virtual void Unload()
		{
			if (!isLoad())
			{
				DGE.Debug.Error(this, Define.EngineErrorName.Resource_CannotUnload(), GetErrorDetail());
			}
			_isLoad = false;
			GC.Collect();
		}

		public bool isLoad()
		{
			return _isLoad;
		}

		public virtual bool GetResource()
		{
			if (isLoad())
			{
				return true;
			}
			else
			{
				DGE.Debug.Error(this, Define.EngineErrorName.Resource_CannotGetResource(), GetErrorDetail());
				return false;
			}
		}

		public void Reload()
		{
			if (isLoad())
			{
				Unload();
				Load();
			}
			else
			{
				DGE.Debug.Error(this, Define.EngineErrorName.Resource_CannotReload(), GetErrorDetail());
			}
		}

		public string filename
		{
			get { return _filename; }
			set
			{
				if (!isLoad())
				{
					_filename = value;
				}
				else
				{
					DGE.Debug.Error(this, Define.EngineErrorName.Resource_CannotChangeFilename(), GetErrorDetail());
				}
			}
		}

		protected string GetErrorDetail()
		{
			return Define.EngineErrorDetail.Filename() + Define.EngineErrorDetail.Separator() + _filename;
		}

		public virtual object Clone()
		{
			Resource res = new Resource();
			res._isLoad = true;
			return res;
		}
	}
}
