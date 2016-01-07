// Potongan Kode Program
    if (zz.Count == 0)
                            {
                                for (int i = 0; i < arrayIsi.Length; i++)

                                    if (zz.ContainsKey(arrayIsi[i]))
                                    {
                                        zz[arrayIsi[i]] = zz[arrayIsi[i]] + 1;
                                    }
                                    else
                                    {
                                        zz.Add(arrayIsi[i], 1);
                                    }
                            }
                            zz.Remove(0);

// ================================================================================ //
                            
                             double labelparalel = 0;
                            float modusparalel = 0;
                            sw = Stopwatch.StartNew();
                            Parallel.ForEach(zz, isi =>
                            {
                                if (modusparalel < isi.Value)
                                {
                                    modusparalel = isi.Value;
                                    labelparalel = isi.Key;
                                }
                                Console.WriteLine("Paralel thread {0} nilainya {1}", Thread.CurrentThread.ManagedThreadId, isi);

                            });
                            //results.Clear();
                            results.Add(labelparalel);
                            Console.WriteLine("hasil modus paralel: {0}", labelparalel);
                            Console.WriteLine("Wktu parael: {0}", sw.Elapsed.TotalMilliseconds);