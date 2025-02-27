SELECT d.NMB_CLM, a.*                
FROM [dbo].[DLVR_TBL] d                INNER JOIN [dbo].[ADDR_TBL] a ON a.DLVR = d.NMB_CLM                WHERE                    d.NMB_CLM = 1
